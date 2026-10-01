import { useEffect, useState } from "react";

import { getBooks } from "../services/bookService";
import type { Book } from "../types/Book";

import BookCard from "../components/books/BookCard";

function ProductsPage() {
  const [books, setBooks] = useState<Book[]>([]);

  const [loading, setLoading] = useState<boolean>(true);

  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    loadBooks();
  }, []);

  async function loadBooks() {
    try {
      setLoading(true);

      const data = await getBooks();

      setBooks(data);
    } catch (error) {
      console.error(error);

      setError("Unable to load books.");
    } finally {
      setLoading(false);
    }
  }

  if (loading) {
    return <p>Loading books...</p>;
  }

  if (error) {
    return <p>{error}</p>;
  }

  return (
    <div>
      <h1>Books</h1>

      {books.map((book) => (
        <BookCard
          key={book.id}
          book={book}
        />
      ))}
    </div>
  );
}

export default ProductsPage;