import apiClient from "./apiClient";

export interface Product {
  id: number;
  title: string;
  author: string;
  price: number;
  category: string;
}

export async function getProducts(): Promise<Product[]> {
  const response = await apiClient.get<Product[]>("/products");

  return response.data;
}