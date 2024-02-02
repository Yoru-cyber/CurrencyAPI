from main import app
from main import currentPrice
from fastapi.testclient import TestClient

client = TestClient(app)
def test_dolar():
    response = client.get("/v1/dolar")
    assert response.status_code == 200, "Correct response should equal status code 200"
    assert response.json() == {"Status": 200, "Message": "OK", "Price": currentPrice} 

if __name__ == "__main__":
   testResult = test_dolar()
if testResult == None:
    print("Test passed /ᐠ｡ꞈ｡ᐟ\\")