namespace WebApiWithGenerics.WebApi.SqlScriptGenerators
{
    using System.Globalization;

    using WebApiWithGenerics.WebApi.Contracts.User;

    public class UserDapperMySqlScriptGenerator : DapperMySqlScriptGenerator<UserDbContract>
    {
        public string GenerateSelectByEmailScript()
        {
            const string conditionText = $"{nameof(UserDbContract.Email)}=@{nameof(UserDbContract.Email)}";
            var scriptText = string.Format(CultureInfo.InvariantCulture, SelectByConditionTemplate, this.EntityName, conditionText);
            return scriptText;
        }
    }
}
