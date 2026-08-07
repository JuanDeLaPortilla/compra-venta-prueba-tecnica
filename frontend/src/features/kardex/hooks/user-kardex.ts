import { useQuery } from "@tanstack/react-query";
import { kardexService } from "../infrastructure/kardex.service";
import { kardexKeys } from "./kardex-keys";

export const useKardex = () => {
  return useQuery({
    queryKey: kardexKeys.all,
    queryFn: () => kardexService.getProducts(),
  });
};