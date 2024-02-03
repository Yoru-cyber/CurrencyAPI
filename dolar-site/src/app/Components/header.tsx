export default function Header() {
    return (

        <header className="container mx-auto" style={{fontFamily: "Space Mono, monospace", fontSize: "20px"}}>
            <ul className="flex justify-center space-x-20">
                <li><a href="#">Home</a></li>
                <li><a href="#">About</a></li>
                <li><a href="#">Contact</a></li>
            </ul>
        </header>
    )
}