namespace WebApiWithGenerics.WebApi.SqlScriptGenerators
{
    using System.Globalization;

    using WebApiWithGenerics.WebApi.Contracts.Todo;

    public class TodoDapperMySqlScriptGenerator : DapperMySqlScriptGenerator<TodoDbContract>
    {
        public string GenerateSelectByTodoListIdScript()
        {
            const string conditionText = $"{nameof(TodoDbContract.TodoListId)}=@{nameof(TodoDbContract.TodoListId)}";
            var scriptText = string.Format(CultureInfo.InvariantCulture, SelectByConditionTemplate, this.EntityName, conditionText);
            return scriptText;
        }
    }
}
