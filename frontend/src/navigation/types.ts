import { Clipboard, ShoppingCart, Store } from "lucide-react";
import type { MenuItem } from "./menu";

export const menuItems: MenuItem[] = [
  {
    title: "Kardex",
    path: "/kardex",
    icon: Clipboard
  },
  {
    title: "Compras",
    path: "/purchases",
    icon: ShoppingCart
  },
  {
    title: "Ventas",
    path: "/sales",
    icon: Store
  }
]