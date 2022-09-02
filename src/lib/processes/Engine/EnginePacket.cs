namespace processes.Engine
{
    public class EnginePacket: IEnginePacket
    {
        private readonly ITypeCaster _typeCaster;

        public object Data{ get; set; }
        public string ParamName{ get; set; }

        public EnginePacket(ITypeCaster typeCaster) => (this._typeCaster) = (typeCaster ?? new TypeCaster());
        public EnginePacket(ITypeCaster typeCaster, object data, string paramName): this(typeCaster) => (this.Data, this.ParamName) = (data, paramName);

        public T Cast<T>() => this._typeCaster.Cast<T>(this.Data);
    }
}