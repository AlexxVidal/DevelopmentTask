using FullStackDeveloperTask.Data.Models;

namespace FullStackDeveloperTask.Api.Models.Extensions
{
    public static class DataModels
    {
        public static GetForkliftResponse ToGetForkliftReponse(this Forklift forklift)
        {
            return new GetForkliftResponse
            {
                Name = forklift.Name,
                Id = forklift.Id,
                ManufacturingDate = forklift.ManufacturingDate,
                ModelNumber = forklift.ModelNumber
            };
        }

        public static CommandExecuteResponse ToCommandExecuteReponse(this Command source)
        {
            return new CommandExecuteResponse
            {
                Error = source.Error,
                Actions = source.Actions?.Select(a => a.ToAction()).ToList()
            };
        }

        public static Action ToAction(this Data.Models.Action a)
        {
            if (a is null) throw new ArgumentNullException(nameof(a));

            return new Action
            {
                Direction = a.Direcction switch
                {
                    'F' => ActionTypes.Forward,
                    'B' => ActionTypes.Backward,
                    'L' => ActionTypes.Left,
                    'R' => ActionTypes.Right,
                },
                Value = a.Value
            };
        }
    }
}
