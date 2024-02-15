import axios from 'axios' //axios makes easy the handling of errors avoiding all the clutter of a complete fetch request
import React, { useEffect } from 'react'
import { useState } from 'react'
const getPrice = (url) =>{
  const [Price, setPrice] = useState(null)
  const [error, setError] = useState(null)
  const [loading, setLoading] = useState(true)
  useEffect(() => {
    const fetchData = async () =>{
      try {
        setLoading(true)
        const {data: response}= await axios.get(url, {headers: {'Content-Type': 'application/json'}})
        setPrice(response.Price)
      } catch (error) {
        setError(error)
      }
      finally{
        setLoading(false)
      }
    }
    fetchData()
  },[url])
  return {Price, loading, error}
}
export default getPrice;