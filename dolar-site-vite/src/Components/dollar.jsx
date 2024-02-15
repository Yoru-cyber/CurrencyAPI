import getPrice from '../services/getPrice';
import { useEffect, useState } from 'react';
export default function Dollar() {
    const url = 'http://localhost:8000/v1/dolar'
    const {Price, loading, error} = getPrice(url)
    if(loading){
        return <h2>Cargando...</h2>
    }
    if(error){
        return <h2>Something went wrong! <br></br> {error}</h2>
    }
    return (
        
            <h2>
                Hoy el precio del dólar es
                <b style={{ color: "#5EC986" }}>{Price}</b>
            </h2>
        
    );
}