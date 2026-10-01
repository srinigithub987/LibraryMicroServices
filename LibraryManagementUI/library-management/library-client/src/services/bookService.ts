import apiClient from "./apiClient";
import type { Book } from "../types/Book";

export async function getBooks(): Promise<Book[]> {
  const response = await apiClient.get<Book[]>("/Books");

  return response.data;
}

export async function getBookById(
  id: number
): Promise<Book> {

  const response =
    await apiClient.get<Book>(`/Books/${id}`);

  return response.data;
}