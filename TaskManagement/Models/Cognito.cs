namespace TaskManagement.Models
{
    public class CognitoSettings
    {
        public string ClientId { get; set; }
        public string UserPoolId { get; set; }
        public string AccessKey { get; set; }
        public string SecretAccessKey { get; set; }
        public string Authority { get; set; }
        public string AuthUri { get; set; }
    }

    public class CognitoCreateUserRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string TemporaryPassword { get; set; }
    }

    public class CognitoChangePasswordRequest
    {
        public string Username { get; set; }
        public string NewPassword { get; set; }
    }

    public class CognitoGetAccessTokenRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}