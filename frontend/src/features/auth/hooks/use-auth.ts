import { jwtDecode } from "jwt-decode";

import { tokenStorage } from "@/core/storage/token-storage";

import type { JwtClaims } from "../domain/auth";

export const useAuth = () => {
  const token = tokenStorage.get();

  if (!token) {
    return {
      isAuthenticated: false,
      username: null,
    };
  }

  try {
    const claims = jwtDecode<JwtClaims>(token);

    return {
      isAuthenticated: true,
      username: claims.username ?? null,
    };
  } catch {
    return {
      isAuthenticated: false,
      username: null,
    };
  }
};