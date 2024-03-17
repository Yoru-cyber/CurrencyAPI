import GetPrice from "../hooks/GetPrice";
export default function Dollar() {
  const url = "http://localhost:5135/v2/dolar";
  const { Price, loading, error } = GetPrice(url);
  if (loading) {
    return <h2 className="text-5xl cd ap">Cargando...</h2>;
  }
  else if (error) {
    return (
      <h2 className="text-5xl">
        Something went wrong!😿 <br></br> {error.message}
      </h2>
    );
  }
  return (
    <h2 className="flex justify-center flex-wrap text-4xl font-noto-serif">
      Hoy el precio del dólar es
      <b style={{ color: "#5EC986" }}>{Price}</b>
    </h2>
  );
}
