using System.Threading.Tasks;
using FluentResults;
using RestSharp;
using Splitwise.Clients.Interfaces;
using Splitwise.Errors;
using Splitwise.Requests.Group;
using Splitwise.Responses.Group;
using Splitwise.Validators.Group;

namespace Splitwise.Clients;

public class GroupClient : IGroupClient
{
    private readonly IRestClient _restClient;

    public GroupClient(IRestClient restClient)
    {
        _restClient = restClient;
    }

    public Task<ListGroupsResponse> ListAsync()
    {
        var restRequest = new RestRequest("get_groups");

        return _restClient.GetAsync<ListGroupsResponse>(restRequest);
    }

    public async Task<Group> GetAsync(int id)
    {
        var restRequest = new RestRequest("get_group/{id}")
            .AddUrlSegment("id", id);

        var getGroupResponse = await _restClient.GetAsync<GetGroupResponse>(restRequest);

        return getGroupResponse.Group;
    }

    public async Task<Result<Group>> CreateAsync(CreateGroupRequest request)
    {
        var result = new Result<Group>();

        var validator = new CreateGroupRequestValidator();

        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
            {
                result.WithError(new ValidationError(error.ErrorMessage));
            }

            return result;
        }

        var restRequest = new RestRequest("create_group")
            .AddJsonBody(request);

        var (group, errors) = await _restClient.PostAsync<CreateGroupResponse>(restRequest);

        if (errors?.Base == null)
        {
            return result.WithValue(group);
        }

        result.WithErrors(errors.Base);

        return result;
    }

    public async Task<Result<bool>> DeleteAsync(int id)
    {
        var restRequest = new RestRequest("delete_group/{id}")
            .AddUrlSegment("id", id);

        var (success, errors) = await _restClient.PostAsync<DeleteGroupResponse>(restRequest);

        if (errors != null)
        {
            return new Result<bool>().WithErrors(errors.Base);
        }

        return Result.Ok(success);
    }

    public async Task<Result<bool>> RestoreAsync(int id)
    {
        var restRequest = new RestRequest("undelete_group/{id}")
            .AddUrlSegment("id", id);

        var (success, errors) = await _restClient.PostAsync<RestoreGroupResponse>(restRequest);

        if (errors != null)
        {
            return new Result<bool>().WithErrors(errors);
        }

        return Result.Ok(success);
    }

    public async Task<Result<bool>> AddUserAsync(BaseAddUserToGroupRequest request)
    {
        var restRequest = new RestRequest("add_user_to_group")
            .AddJsonBody(request);

        var (success, _, errors) = await _restClient.PostAsync<AddUserToGroupResponse>(restRequest);

        if (errors != null)
        {
            return new Result<bool>().WithErrors(errors.Base);
        }

        return Result.Ok(success);
    }

    public async Task<Result<bool>> RemoveUserAsync(RemoveUserFromGroupRequest request)
    {
        var restRequest = new RestRequest("remove_user_from_group")
            .AddJsonBody(request);

        var (success, errors) = await _restClient.PostAsync<RemoveUserFromGroupResponse>(restRequest);

        if (errors != null)
        {
            return new Result<bool>().WithErrors(errors.Base);
        }

        return Result.Ok(success);
    }
}