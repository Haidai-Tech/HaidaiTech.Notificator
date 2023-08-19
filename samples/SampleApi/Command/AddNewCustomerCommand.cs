using MediatR;

namespace SampleApi.Command
{
    public class AddNewCustomerCommand
        : IRequest<string>
    {
        public string Name { get; set; } = default!;
        public int Age { get; set; }
    }
}