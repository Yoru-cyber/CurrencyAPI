import requests
from bs4 import BeautifulSoup
from fastapi import FastAPI
from fastapi.testclient import TestClient

app = FastAPI()

r= requests.get('https://themoneyconverter.com/es/USD/VES')
soup = BeautifulSoup(r.text, 'html.parser')
dolar=soup.find('span')
dolar = dolar.text
arr = dolar.split()
Price = float(arr[2].replace(',','.'))
currentPrice = float(Price)

@app.get("/v1/dolar", status_code=200)
async def root():
    return {"Status": 200, "Message": "OK", "Price": currentPrice}


if __name__ == "__main__":
    app.run()