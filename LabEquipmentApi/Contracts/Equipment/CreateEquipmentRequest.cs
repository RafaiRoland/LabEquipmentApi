using FluentValidation;

namespace LabEquipmentApi.Contracts;

public class CreateEquipmentRequest
{
    public required string Name { get; set; }
    public required string Room { get; set; }
}

public class CreateEquipmentRequestValidator : AbstractValidator<CreateEquipmentRequest>
{
    public CreateEquipmentRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Room)
            .NotEmpty()
            .MaximumLength(20);
    }
}