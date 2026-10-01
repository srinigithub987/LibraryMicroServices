import { Link } from "react-router-dom";

function Header() {
  return (
    <header>
      <h1>Library Management</h1>

      <nav>
        <Link to="/">Home</Link>{" "}
        <Link to="/products">Books</Link>
      </nav>
    </header>
  );
}

export default Header;