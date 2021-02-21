namespace VyasApi.Data
{
	public class VyasConfig
	{
		public OktaConfig Okta {get;set;}
		public int ActivationPeriodInMins {get;set;}
	}

	public class OktaConfig
	{
		public string ClientId {get;set;}
		public string ClientSecret {get;set;}
		public string Authority {get;set;}
		public string Audience {get;set;}
	}
}