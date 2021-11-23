namespace WebApiWithGenerics.WebApi.SqlScriptGenerators
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;

    using WebApiWithGenerics.WebApi.Contracts.Common;

    public class DapperMySqlScriptGenerator<TDbContract>
    {
        protected const string SelectByConditionTemplate = "SELECT * FROM {0} WHERE {1}";

        private const string InsertTemplate = "INSERT INTO {0}({1}) VALUES({2});";

        private const string SelectAllTemplate = "SELECT * FROM {0}";

        private const string UpdateTemplate = "UPDATE {0} SET {1} WHERE {2}";

        private const string DeleteTemplate = "DELETE FROM {0} WHERE {1}";

        private const string IdConditionText = $"{nameof(IWithId.Id)}=@{nameof(IWithId.Id)}";

        protected DapperMySqlScriptGenerator()
        {
            this.EntityName = DbContract<TDbContract>.GetEntityName();
        }

        protected string EntityName { get; }

        public string GenerateInsertScript()
        {
            var propertyNames = GetContractPropertyNames().ToList();

            var propertiesText = string.Join(", ", propertyNames);
            var propertiesValuesText = string.Join(", ", propertyNames.Select(name => $"@{name}"));

            var scriptText = string.Format(CultureInfo.InvariantCulture, InsertTemplate, this.EntityName, propertiesText, propertiesValuesText);
            return scriptText;
        }

        public string GenerateSelectAllScript()
        {
            var scriptText = string.Format(CultureInfo.InvariantCulture, SelectAllTemplate, this.EntityName);
            return scriptText;
        }

        public string GenerateSelectByIdScript()
        {
            var scriptText = string.Format(CultureInfo.InvariantCulture, SelectByConditionTemplate, this.EntityName, IdConditionText);
            return scriptText;
        }

        public string GenerateUpdateScript()
        {
            var propertyNames = GetContractPropertyNames();
            var setValuesText = string.Join(", ", propertyNames.Select(name => $"{name}=@{name}"));

            var scriptText = string.Format(CultureInfo.InvariantCulture, UpdateTemplate, this.EntityName, setValuesText, IdConditionText);
            return scriptText;
        }

        public string GenerateDeleteScript()
        {
            var scriptText = string.Format(CultureInfo.InvariantCulture, DeleteTemplate, this.EntityName, IdConditionText);
            return scriptText;
        }

        private static IEnumerable<string> GetContractPropertyNames()
        {
            var properties = typeof(TDbContract).GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(info => info.Name);
            return properties;
        }
    }
}
