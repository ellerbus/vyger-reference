using System.Linq;
using Augment;
using FluentValidation;
using vyger.Core.Models;
using vyger.Core.Services;

namespace vyger.Core.Validators
{
    public class ExerciseValidator : AbstractValidator<Exercise>
    {
        private IExerciseGroupService _groups;
        private IExerciseCategoryService _categories;
        private IExerciseService _service;

        public ExerciseValidator(
            IExerciseGroupService groups,
            IExerciseCategoryService categories,
            IExerciseService service)

        {
            _groups = groups;
            _categories = categories;
            _service = service;

            CascadeMode = CascadeMode.Continue;

            RuleFor(x => x.GroupId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Length(2)
                .Matches(Constants.Regex.IdPatterns.ExerciseGroup)
                .Must(BeValidGroup).WithMessage("{PropertyName} {PropertyValue} cannot be found");

            RuleFor(x => x.CategoryId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Length(1)
                .Matches(Constants.Regex.IdPatterns.ExerciseCategory)
                .Must(BeValidCateogry).WithMessage("{PropertyName} {PropertyValue} cannot be found");

            RuleFor(x => x.Id)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Length(8)
                .Matches(Constants.Regex.IdPatterns.Exercise)
                .Must(ContainGroupId).WithMessage("{PropertyName} must contain the Group ID")
                .Must(ContainCategoryId).WithMessage("{PropertyName} must contain the Category ID");

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Length(1, 30)
                .Must(NotContainCategory).WithMessage("{PropertyName} not contain the category")
                .Must(BeUnique).WithMessage("{PropertyName} must be unique, {PropertyValue} already in use");
        }

        private bool BeValidGroup(Exercise context, string arg)
        {
            bool exists = _groups.GetExerciseGroups().ContainsPrimaryKey(arg);

            return exists;
        }

        private bool BeValidCateogry(Exercise context, string arg)
        {
            bool exists = _categories.GetExerciseCategories().ContainsPrimaryKey(arg);

            return exists;
        }

        private bool ContainGroupId(Exercise context, string arg)
        {
            return context.GroupId == arg.Substring(0, 2);
        }

        private bool ContainCategoryId(Exercise context, string arg)
        {
            return context.CategoryId == arg.Substring(3, 1);
        }

        private bool NotContainCategory(Exercise context, string arg)
        {
            bool exists = _categories.GetExerciseCategories().ContainsPrimaryKey(context.CategoryId);

            if (exists)
            {
                ExerciseCategory category = _categories.GetExerciseCategories().GetByPrimaryKey(context.CategoryId);

                return arg.StartsWithNotSameAs(category.Name);
            }

            return true;
        }

        private bool BeUnique(Exercise context, string arg)
        {
            ExerciseCollection exercises = _service.GetExercises();

            bool exists = exercises.Any(x => x.GroupId == context.GroupId && x.CategoryId == context.CategoryId && x.Id != context.Id && x.Name.IsSameAs(arg));

            return !exists;
        }
    }
}
