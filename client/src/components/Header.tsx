import { Link } from "react-router-dom";

export default function Header() {
  return (
    <div className="w-full bg-blue-500 text-white p-4 fixed top-0 left-0 shadow-md flex justify-center items-center">
      <Link
        to="/"
        className="text-xl font-bold hover:cursor-pointer hover:text-black text-white mx-4"
      >
        Home
      </Link>

      <Link
        to="/cash"
        className="text-xl font-bold hover:cursor-pointer hover:text-black text-white mx-4"
      >
        Cash
      </Link>

      <Link
        to="/stocks"
        className="text-xl font-bold hover:cursor-pointer hover:text-black text-white mx-4"
      >
        Stocks
      </Link>

      <Link
        to="/crypto"
        className="text-xl font-bold hover:cursor-pointer hover:text-black text-white mx-4"
      >
        Crypto
      </Link>
    </div>
  );
}
