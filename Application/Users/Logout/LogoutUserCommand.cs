using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Users.Logout;

public sealed record LogoutUserCommand(string UserId) : ICommand<string>;
