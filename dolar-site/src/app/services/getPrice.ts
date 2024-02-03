const getPrice = async () =>{
  const res = await fetch('http://localhost:8000/v1/dolar', { cache: 'force-cache', next: {revalidate: 10}})
 
  if (!res.ok) {
    
    throw new Error('Failed to fetch data')
  }
 
  return res.json()
}
export default getPrice;