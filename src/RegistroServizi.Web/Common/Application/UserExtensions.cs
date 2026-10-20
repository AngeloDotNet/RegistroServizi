namespace RegistroServizi.Web.Common.Application;

/// <summary>
/// Provides authorization helpers for enabling or disabling user-interface actions
/// according to the currently authenticated Blazor user.
/// </summary>
/// <param name="authenticationStateProvider">
/// The provider used to retrieve the current user's authentication state.
/// </param>
/// <remarks>
/// This class centralizes role-based UI access checks so components can consistently
/// hide or disable actions when the user is unauthenticated or lacks the required role.
/// The result controls presentation only; server-side authorization must still protect
/// the underlying operation.
/// </remarks>
public sealed class UserExtensions(AuthenticationStateProvider authenticationStateProvider)
{
    /// <summary>
    /// Determines whether an action should be disabled for the current user.
    /// </summary>
    /// <param name="roles">
    /// The roles that are allowed to use the action. The action remains disabled when
    /// the collection is <see langword="null" />, empty, or contains no matching role.
    /// </param>
    /// <returns>
    /// A <see cref="Task{TResult}" /> that contains <see langword="true" /> when the
    /// action should be disabled; otherwise, <see langword="false" />.
    /// </returns>
    /// <remarks>
    /// An authenticated user is allowed to use the action when they belong to at least
    /// one non-empty role in <paramref name="roles" />. This method is intended for
    /// controlling Blazor component state and does not replace authorization checks
    /// performed by the service or server handling the operation.
    /// </remarks>
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

        for (var i = 0; i < roles.Length; i++)
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