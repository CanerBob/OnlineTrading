namespace Trading.Service.Layer.Email;
public class SmtpEmailSender : IEmailSender
{
	private string _host;
	private int _port;
	private bool _enableSSl;
	private string _userName;
	private string _password;
	public SmtpEmailSender(string host, int port, bool enableSSl, string userName, string password)
	{
		this._host = host;
		this._port = port;
		this._enableSSl = enableSSl;
		this._userName = userName;
		this._password = password;
	}
	public Task SendEmailAsync(string email, string subject, string htmlMessage)
	{
		var client = new SmtpClient(this._host, this._port)
		{
			Credentials = new NetworkCredential(_userName, _password),
			EnableSsl = this._enableSSl
		};
			return client.SendMailAsync
			(
			new MailMessage(this._userName, email, subject, htmlMessage) 
			{
			IsBodyHtml = true
			}
			);
	}
}