import requests
from bs4 import BeautifulSoup
from fastapi import FastAPI
from fastapi.testclient import TestClient
from fastapi.middleware.cors import CORSMiddleware
app = FastAPI()
origins = [
    "http://localhost:5173",
]

app.add_middleware(
    CORSMiddleware,
    allow_origins=origins,
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

r= requests.get('https://www.bcv.org.ve/', verify=False)
soup = BeautifulSoup(r.text, 'html.parser')
dolar=soup.find(id='dolar').find('strong')
dolar = dolar.text
Price = float(dolar.replace(',', '.'))
currentPrice = float(Price)

@app.get("/v1/dolar", status_code=200)
async def root():
    return {"Status": 200, "Message": "OK", "Price": currentPrice}


if __name__ == "__main__":
    app.run()
