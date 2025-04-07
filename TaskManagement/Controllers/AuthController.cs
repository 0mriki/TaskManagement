using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Runtime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TaskManagement.Models;

namespace TaskManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly CognitoSettings _cognitoSettings;
        private readonly AmazonCognitoIdentityProviderClient _cognitoClient;


        public AuthController(ILogger<AuthController> logger, IOptions<CognitoSettings> cognitoOptions)
        {
            _logger = logger;
            _cognitoSettings = cognitoOptions.Value;
            BasicAWSCredentials credentials = new BasicAWSCredentials(_cognitoSettings.AccessKey, _cognitoSettings.SecretAccessKey);
            _cognitoClient = new AmazonCognitoIdentityProviderClient(credentials, RegionEndpoint.EUNorth1);
        }
        /// <summary>
        /// User signup for cognito
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("signUp")]
        public async Task<IActionResult> CreateUser([FromBody] CognitoCreateUserRequest request)
        {
            try
            {
                var createRequest = new AdminCreateUserRequest
                {
                    UserPoolId = _cognitoSettings.UserPoolId,
                    Username = request.Username,
                    TemporaryPassword = request.TemporaryPassword,
                    UserAttributes = new List<AttributeType>
                    {
                        new AttributeType { Name = "email", Value = request.Email },
                        new AttributeType { Name = "name", Value = request.FullName }
                    },
                    MessageAction = MessageActionType.SUPPRESS
                };

                var response = await _cognitoClient.AdminCreateUserAsync(createRequest);
                _logger.LogInformation($"User with username {request.Username} created successfully");
                return Ok("User created successfully. Note: your password is temporary. Please change it to a permanant password.");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while creating user", ex.Message);
                return StatusCode(500);
            }
        }
        /// <summary>
        /// Changes cognito user's temporary password. Note: it also confirms the user. 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("changeTempPassword")]
        public async Task<IActionResult> ChangePassword([FromBody] CognitoChangePasswordRequest request)
        {
            try
            {
                var changePasswordRequest = new AdminSetUserPasswordRequest
                {
                    UserPoolId = _cognitoSettings.UserPoolId,
                    Username = request.Username,
                    Password = request.NewPassword,
                    Permanent = true // Set the new password as permanent
                };

                await _cognitoClient.AdminSetUserPasswordAsync(changePasswordRequest);
                _logger.LogInformation($"Password for user {request.Username} changed successfully");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while changing password", ex.Message);
                return StatusCode(500);
            }
        }
        /// <summary>
        /// Returns access token by username and password authorization via cognito
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Access token</returns>
        [HttpPost("getAccessToken")]
        public async Task<IActionResult> GetAccessTokenAsync([FromBody] CognitoGetAccessTokenRequest request)
        {
            try
            {
                var authRequest = new AdminInitiateAuthRequest
                {
                    UserPoolId = _cognitoSettings.UserPoolId,
                    ClientId = _cognitoSettings.ClientId,
                    AuthFlow = AuthFlowType.ADMIN_NO_SRP_AUTH, // Use username/password for auth
                    AuthParameters = new Dictionary<string, string>
                    {
                        { "USERNAME", request.Username },
                        { "PASSWORD", request.Password }
                    }
                };

                var authResponse = await _cognitoClient.AdminInitiateAuthAsync(authRequest);

                if (authResponse.AuthenticationResult != null)
                {
                    return Ok(authResponse.AuthenticationResult.AccessToken);
                }
                else
                {
                    return BadRequest("Authentication failed");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting access token of user {request.Username}");
                return StatusCode(500);
            }
        }
    }
}

