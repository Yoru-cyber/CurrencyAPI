import Header from "./Components/header";
import Image from "next/image";
import Dollar from "./Components/dollar";
export default function Home() {
  
  return (
    <>
      <Header />
      <div
        className="flex justify-center items-center m-20"
        style={{ fontFamily: "Space Mono, monospace", height: "50vh" }}
      >
        <Dollar />
        <Image
          src={"/capybara.png"}
          width={200}
          height={200}
          priority={true}
          alt="dolar"
        />
      </div>
    </>
  );
}
