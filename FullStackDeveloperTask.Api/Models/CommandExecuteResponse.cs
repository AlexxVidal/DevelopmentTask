namespace FullStackDeveloperTask.Api.Models
{
    public class CommandExecuteResponse
    {
        public List<Action>? Actions { get; set; } = null;

        public string? Error { get; set; } = null;
    }
}
