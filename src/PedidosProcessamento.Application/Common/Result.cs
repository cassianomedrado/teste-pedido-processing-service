using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PedidosProcessamento.Application.Common
{
    public record ResultError(string Code, string Message);

    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T? Value { get; }
        public ResultError? Error { get; }

        private Result(bool isSuccess, T? value, ResultError? error)
        {
            IsSuccess = isSuccess;
            Value = value;
            Error = error;
        }

        public static Result<T> Success(T value)
            => new(true, value, null);

        public static Result<T> Failure(ResultError error)
            => new(false, default, error);
    }

    public static class ResultExtensions
    {
        public static IActionResult ToRestResult<T>(
            this Result<T> result,
            ControllerBase controller)
        {
            if (result.IsSuccess)
            {
                return controller.Created(
                    string.Empty,
                    new { id = result.Value });
            }

            return result.Error!.Code switch
            {
                "VALIDATION_ERROR" =>
                    controller.BadRequest(new { error = result.Error.Message }),

                "CONFLICT" =>
                    controller.Conflict(new { error = result.Error.Message }),

                _ =>
                    controller.StatusCode(
                        StatusCodes.Status500InternalServerError,
                        new { error = "Erro interno" })
            };
        }
    }
}