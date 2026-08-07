import { apiClient } from "@/core/http/api-client";
import type { CreatePurchaseRequest, Purchase } from "../domain/purchase";
import type { ApiResponse } from "@/core/http/api-response";

export const purchaseService = {
  async getPurchases(): Promise<Purchase[]> {
    const response =
      await apiClient.get<ApiResponse<Purchase[]>>("/purchases")

    return response.data.data!;
  },

  async createPurchase(
    request: CreatePurchaseRequest,
  ): Promise<ApiResponse<Purchase>> {
    const response =
      await apiClient.post<ApiResponse<Purchase>>(
        "/purchases",
        request,
      );

    return response.data;
  },
}