using FluentValidation;
using MindMiners.Models;
using System.IO;

namespace MindMiners.Validators
{
    public class FileInputModelValidator : AbstractValidator<FileInputModel>
    {
        public FileInputModelValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            When(c => c != null, () =>
            {
                RuleFor(x => x.FileToUpload)
                    .NotNull().WithMessage("Arquivo não selecionado")
                    .Must(y => y.Length > 0).WithMessage("Arquivo não selecionado")
                    .Must(y => Path.GetExtension(y.FileName).Equals(".srt")).WithMessage("Extensão inválida! Escolha um arquivo .srt");

                RuleFor(x => x.Offset)
                    .NotNull().WithMessage("Offset inválido")
                    .Must(c => double.TryParse(c, out double result)).WithMessage("Offset com formato inválido");
            });
        }
    }
}
