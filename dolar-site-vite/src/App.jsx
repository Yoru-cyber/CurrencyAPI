import "./output.css";
import Header from "./Components/header";
import Dollar from "./Components/dollar";

function App() {
  return (
    <>
      <Header />
      <div
        className="flex flex-wrap justify-center items-center m-20"
        style={{ height: "50vh" }}
      >
        <Dollar />
        <img src="/capybara.png"></img>
      </div>
    </>
  );
}

export default App;
