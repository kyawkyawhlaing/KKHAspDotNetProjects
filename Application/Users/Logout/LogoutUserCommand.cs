using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Users.Logout;

public sealed record LogoutUserCommand(Guid UserId) : ICommand<string>;
