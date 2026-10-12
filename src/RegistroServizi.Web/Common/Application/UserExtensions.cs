namespace RegistroServizi.Web.Common.Application;

public sealed class UserExtensions(AuthenticationStateProvider authenticationStateProvider)
{
    //public async Task<bool> ModifyRoleDisabledAsync(string role)
    //{
    //    var authState = await authenticationStateProvider.GetAuthenticationStateAsync().ConfigureAwait(false);
    //    var user = authState.User;

    //    return !user.IsInRole(role);
    //}

    public async Task<bool> ManageButtonDisabledAsync(string[] roles)
    {
        if (roles == null || roles.Length == 0)
        {
            return true;
        }

        var authState = await authenticationStateProvider.GetAuthenticationStateAsync().ConfigureAwait(false);
        var user = authState.User;

        if (user?.Identity?.IsAuthenticated != true)
        {
            return true;
        }

        for (int i = 0; i < roles.Length; i++)
        {
            var role = roles[i];

            if (!string.IsNullOrEmpty(role) && user.IsInRole(role))
            {
                return false;
            }
        }

        return true;
    }
}