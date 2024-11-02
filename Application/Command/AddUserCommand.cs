using Core.DTos;
using Core.Interfaces;
using Infrastructure.EF;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Command
{
    public class AddUserCommand : IRequest<AddUserResponse>
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
    public class AddUserResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, AddUserResponse>
    {
        private readonly IFinRepository _finRepository;
        public AddUserCommandHandler(IFinRepository finRepository)
        {
            _finRepository = finRepository;
        }
        public Task<AddUserResponse> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = new UserDto
                {
                    UserID = request.UserID,
                    UserName = request.UserName,
                    Email = request.Email,
                    Password = request.Password,
                    IsActive = request.IsActive
                };
                var data= _finRepository.AddUser(user);
                return Task.FromResult(new AddUserResponse { IsSuccess = true, Message = "User added successfully" });

            }
            catch (Exception ex)
            {
                return Task.FromResult(new AddUserResponse { IsSuccess = false, Message = ex.Message });
            }
        }
    }
}
