using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TechChallenge.Core.Models;

namespace TechChallenge.Tests
{
    public class ContatoTest
    {
        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }

        [Fact]
        public void Contato_Validate_NomeNulo()
        {
            var errors = ValidateModel(new ContatoInsertModel()
            {
                DDD = 11,
                EMail = "email@dominio.com",
                Nome = null,
                Telefone = "12345678"
            });

            //Assert.Contains(errors, (e) => e.MemberNames.Contains("Nome") && (e.ErrorMessage?.Contains("Nome é obrigatório", StringComparison.InvariantCultureIgnoreCase) ?? false));
            Assert.Contains(new("Nome é obrigatório", ["Nome"]), errors, new ComparerValidationResult());
        }

        [Fact]
        public void Contato_Validate_NomeVazio()
        {
            var errors = ValidateModel(new ContatoInsertModel()
            {
                DDD = 11,
                EMail = "email@dominio.com",
                Nome = string.Empty,
                Telefone = "12345678"
            });
           
            Assert.Contains(errors, (e) => e.MemberNames.Contains("Nome") && (e.ErrorMessage?.Contains("Nome é obrigatório") ?? false));
        }

        [Fact]
        public void Contato_Validate_EmailSemArroba()
        {
            var errors = ValidateModel(new ContatoInsertModel()
            {
                DDD = 11,
                EMail = "emaildominio.com",
                Nome = "Nome Sobrenome",
                Telefone = "12345678"
            });

            Assert.Contains(errors, (e) => e.MemberNames.Contains("EMail") && (e.ErrorMessage?.Contains("E-mail inválido") ?? false));
        }

        [Fact]
        public void Contato_Validate_EmailSemDominio()
        {
            var errors = ValidateModel(new ContatoInsertModel()
            {
                DDD = 11,
                EMail = "email@.com",
                Nome = "Nome Sobrenome",
                Telefone = "12345678"
            });

            Assert.Contains(errors, (e) => e.MemberNames.Contains("EMail") && (e.ErrorMessage?.Contains("E-mail inválido") ?? false));
        }

        [Fact]
        public void Contato_Validate_EmailSemRegiao()
        {
            var errors = ValidateModel(new ContatoInsertModel()
            {
                DDD = 11,
                EMail = "email@dominio",
                Nome = "Nome Sobrenome",
                Telefone = "12345678"
            });

            Assert.Contains(errors, (e) => e.MemberNames.Contains("EMail") && (e.ErrorMessage?.Contains("E-mail inválido") ?? false));
        }

        [Fact]
        public void Contato_Validate_DDD_Menor()
        {
            var errors = ValidateModel(new ContatoInsertModel()
            {
                DDD = 1,
                EMail = "email@dominio.com",
                Nome = "Nome Sobrenome",
                Telefone = "12345678"
            });

            Assert.Contains(errors, (e) => e.MemberNames.Contains("DDD") && (e.ErrorMessage?.Contains("Região inválida") ?? false));
        }

        [Fact]
        public void Contato_Validate_DDD_Maior()
        {
            var errors = ValidateModel(new ContatoInsertModel()
            {
                DDD = 100,
                EMail = "email@dominio.com",
                Nome = "Nome Sobrenome",
                Telefone = "12345678"
            });

            Assert.Contains(errors, (e) => e.MemberNames.Contains("DDD") && (e.ErrorMessage?.Contains("Região inválida") ?? false));
        }

        [Fact]
        public void Contato_Validate_Telefone_TamanhoMenor()
        {
            var errors = ValidateModel(new ContatoInsertModel()
            {
                DDD = 11,
                EMail = "email@dominio.com",
                Nome = "Nome Sobrenome",
                Telefone = "1234567"
            });

            Assert.Contains(errors, (e) => e.MemberNames.Contains("Telefone") && (e.ErrorMessage?.Contains("Telefone inválido") ?? false));
        }

        [Fact]
        public void Contato_Validate_Telefone_TamanhoMaior()
        {
            var errors = ValidateModel(new ContatoInsertModel()
            {
                DDD = 11,
                EMail = "email@dominio.com",
                Nome = "Nome Sobrenome",
                Telefone = "1234567890"
            });

            Assert.Contains(errors, (e) => e.MemberNames.Contains("Telefone") && (e.ErrorMessage?.Contains("Telefone inválido") ?? false));
        }
    }

    class ComparerValidationResult : IEqualityComparer<ValidationResult>
    {
        public bool Equals(ValidationResult? x, ValidationResult? y) => (x.ErrorMessage == y.ErrorMessage && x.MemberNames.All(y.MemberNames.Contains));

        public int GetHashCode([DisallowNull] ValidationResult obj)
        {
            throw new NotImplementedException();
        }
    }
}