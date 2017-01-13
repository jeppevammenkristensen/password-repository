namespace Api.Models.Commands
{
    public class AddPasswordCommand
    {
        public string Password { get; set; }
        public string Environment { get; set; }
        public string Company{ get; set; }
        public string User { get; set; }
    }
}