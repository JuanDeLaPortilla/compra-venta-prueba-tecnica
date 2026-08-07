import type { ApiResponse } from "@/core/http/api-response";
import { apiClient } from "@/core/http/api-client";
import type {
  CreateProductRequest,
  Product,
} from "../domain/product";

export const productService = {
  async getAll(): Promise<Product[]> {
    const response =
      await apiClient.get<ApiResponse<Product[]>>("/products");

    return response.data.data!;
  },

  async create(
    request: CreateProductRequest,
  ): Promise<ApiResponse<Product>> {
    const response =
      await apiClient.post<ApiResponse<Product>>("/products", request);

    return response.data;
  },
};