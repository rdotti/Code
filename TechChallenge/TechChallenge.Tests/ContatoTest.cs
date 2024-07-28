using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TechChallenge.Core.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TechChallenge.Tests
{
    public class ContatoTest
    {
        private readonly ComparerValidationResult _comparer = new();
        
        private static List<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }

        [Fact]
        public void Contato_Validate_NomeNulo()
        {
            var error = ValidateModel(new ContatoInsertModel()
            {
                DDD = 11,
                EMail = "email@dominio.com",
                Nome = null,
                Telefone = "12345678"
            }).First();

            //Assert.Contains(errors, (e) => e.MemberNames.Contains("Nome") && (e.ErrorMessage?.Contains("Nome � obrigat�rio", StringComparison.InvariantCultureIgnoreCase) ?? false));
            Assert.Equal(new("Nome � obrigat�rio", ["Nome"]), error, _comparer);
        }

        [Fact]
        public void Contato_Validate_NomeVazio()
        {
            var error = ValidateModel(new ContatoInsertModel()
            {
                DDD = 11,
                EMail = "email@dominio.com",
                Nome = string.Empty,
                Telefone = "12345678"
            }).First();

            //Assert.Contains(errors, (e) => e.MemberNames.Contains("Nome") && (e.ErrorMessage?.Contains("Nome � obrigat�rio") ?? false));
            Assert.Equal(new("Nome � obrigat�rio", ["Nome"]), error, _comparer);
        }

        [Fact]
        public void Contato_Validate_EmailSemArroba()
        {
            var error = ValidateModel(new ContatoInsertModel()
            {
                DDD = 11,
                EMail = "emaildominio.com",
                Nome = "Nome Sobrenome",
                Telefone = "12345678"
            }).First();

            //Assert.Contains(errors, (e) => e.MemberNames.Contains("EMail") && (e.ErrorMessage?.Contains("E-mail inv�lido") ?? false));
            Assert.Equal(new("E-mail inv�lido", ["EMail"]), error, _comparer);
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

            Assert.Contains(errors, (e) => e.MemberNames.Contains("EMail") && (e.ErrorMessage?.Contains("E-mail inv�lido") ?? false));
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

            Assert.Contains(errors, (e) => e.MemberNames.Contains("EMail") && (e.ErrorMessage?.Contains("E-mail inv�lido") ?? false));
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

            Assert.Contains(errors, (e) => e.MemberNames.Contains("DDD") && (e.ErrorMessage?.Contains("Regi�o inv�lida") ?? false));
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

            Assert.Contains(errors, (e) => e.MemberNames.Contains("DDD") && (e.ErrorMessage?.Contains("Regi�o inv�lida") ?? false));
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

            Assert.Contains(errors, (e) => e.MemberNames.Contains("Telefone") && (e.ErrorMessage?.Contains("Telefone inv�lido") ?? false));
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

            Assert.Contains(errors, (e) => e.MemberNames.Contains("Telefone") && (e.ErrorMessage?.Contains("Telefone inv�lido") ?? false));
        }
    }

    class ComparerValidationResult : IEqualityComparer<ValidationResult>
    {
        public bool Equals(ValidationResult? x, ValidationResult? y) =>
            x.ErrorMessage == y.ErrorMessage && x.MemberNames.SequenceEqual(y.MemberNames);

        public int GetHashCode([DisallowNull] ValidationResult obj) =>
            obj.GetHashCode();
    }
}