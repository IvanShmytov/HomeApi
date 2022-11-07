using FluentValidation;
using HomeApi.Contracts.Models.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeApi.Contracts.Validation
{
    public class EditRoomRequestValidator : AbstractValidator<EditRoomRequest>
    {
        public EditRoomRequestValidator()
        {
            // Закомментированное правило валидации для Name оставлено, чтобы показать, что я умею этим пользоваться,
            // но решил убрать его специально,чтобы у пользователя была возможность не вводить новое значение
            // для этого свойства.
            //RuleFor(x => x.Name).Must(BeSupported).WithMessage($"Please choose one of the following names: {string.Join(", ", Values.ValidRooms)}"); ;
            RuleFor(x => x.Area).NotEmpty();
            RuleFor(x => x.Voltage).NotEmpty().InclusiveBetween(120, 220);
            RuleFor(x => x.GasConnected).NotNull();
        }
        private bool BeSupported(string name)
        {
            return Values.ValidRooms.Any(e => e == name);
        }
    }
}
