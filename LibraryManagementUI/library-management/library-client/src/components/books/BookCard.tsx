import type { Book } from "../../types/Book";

interface BookCardProps {
  book: Book;
}

function BookCard({ book }: BookCardProps) {
  return (
    <div>
      <h2>{book.title}</h2>

      <p>
        Author: {book.author}
      </p>

      <p>
        Category: {book.category}
      </p>

      <p>
        Price: ₹{book.price}
      </p>

      <button>
        View Details
      </button>

      <button>
        Add to Cart
      </button>
    </div>
  );
}

export default BookCard;