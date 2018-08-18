using System.Linq;
using Augment;
using FluentValidation;
using vyger.Core.Models;
using vyger.Core.Services;

namespace vyger.Core.Validators
{
    public class ExerciseGroupValidator : AbstractValidator<ExerciseGroup>
    {
        private IExerciseGroupService _service;

        public ExerciseGroupValidator(IExerciseGroupService service)
        {
            _service = service;

            RuleFor(x => x.Id)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Length(2)
                .Matches(Constants.Regex.IdPatterns.ExerciseGroup);

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Length(1, 30)
                .Must(BeUnique).WithMessage("{PropertyName} must be unique, {PropertyValue} already in use");
        }

        private bool BeUnique(ExerciseGroup context, string arg)
        {
            ExerciseGroupCollection groups = _service.GetExerciseGroups();

            bool exists = groups.Any(x => x.Id != context.Id && x.Name.IsSameAs(arg));

            return !exists;
        }
    }
}
