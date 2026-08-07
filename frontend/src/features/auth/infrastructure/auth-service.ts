import { apiClient } from "@/core/http/api-client";
import type {
  LoginRequest,
  LoginResponse,
} from "../domain/auth";

export const authService = {
  async login(
    request: LoginRequest
  ): Promise<LoginResponse> {
    const response = await apiClient
      .post<LoginResponse>("/auth/public/login", request);

    return response.data;
  },
};