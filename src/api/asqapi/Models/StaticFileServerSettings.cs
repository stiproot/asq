namespace asqapi.Models
{
  public sealed class StaticFileServerSettings
  {
    public string ImgBasePath{ get; set; }
    public string VideoBasePath{ get; set; }
    public string StaticImgServerBaseUrl{ get; set; }
    public string StaticVideoServerBaseUrl{ get; set; }
  }
}