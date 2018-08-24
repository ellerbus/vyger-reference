using System.Linq;
using Augment;
using FluentValidation;
using vyger.Core.Models;
using vyger.Core.Services;

namespace vyger.Core.Validators
{
    public class ExerciseCategoryValidator : AbstractValidator<ExerciseCategory>
    {
        private IExerciseCategoryService _service;

        public ExerciseCategoryValidator(IExerciseCategoryService service)
        {
            _service = service;

            CascadeMode = CascadeMode.Continue;

            RuleFor(x => x.Id)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Length(1)
                .Matches(Constants.Regex.IdPatterns.ExerciseCategory);

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Length(1, 30)
                .Must(BeUnique).WithMessage("{PropertyName} must be unique, {PropertyValue} already in use");
        }

        private bool BeUnique(ExerciseCategory context, string arg)
        {
            ExerciseCategoryCollection categories = _service.GetExerciseCategories();

            bool exists = categories.Any(x => x.Id != context.Id && x.Name.IsSameAs(arg));

            return !exists;
        }
    }
}
