"use server";
import getPrice from "../services/getPrice";
export default async function Dollar() {
    const data = await getPrice();
    return (

            <h2 className="text-5xl">
                Hoy el precio del dólar es
                <b style={{ color: "#5EC986" }}> {data.Price}</b>
            </h2>
        
    );
}