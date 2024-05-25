using System.ComponentModel.DataAnnotations;
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

            Assert.True(errors.Any(v => v.MemberNames.Contains("Nome") && v.ErrorMessage.Contains("Nome é obrigatório")));
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

            Assert.True(errors.Any(v => v.MemberNames.Contains("Nome") && v.ErrorMessage.Contains("Nome é obrigatório")));
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

            Assert.True(errors.Any(v => v.MemberNames.Contains("EMail") && v.ErrorMessage.Contains("E-mail inválido")));
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

            Assert.True(errors.Any(v => v.MemberNames.Contains("EMail") && v.ErrorMessage.Contains("E-mail inválido")));
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

            Assert.True(errors.Any(v => v.MemberNames.Contains("EMail") && v.ErrorMessage.Contains("E-mail inválido")));
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

            Assert.True(errors.Any(v => v.MemberNames.Contains("DDD") && v.ErrorMessage.Contains("Região inválida")));
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

            Assert.True(errors.Any(v => v.MemberNames.Contains("DDD") && v.ErrorMessage.Contains("Região inválida")));
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

            Assert.True(errors.Any(v => v.MemberNames.Contains("Telefone") && v.ErrorMessage.Contains("Telefone inválido")));
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

            Assert.True(errors.Any(v => v.MemberNames.Contains("Telefone") && v.ErrorMessage.Contains("Telefone inválido")));
        }
    }
}