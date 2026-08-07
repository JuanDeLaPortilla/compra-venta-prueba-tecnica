import { ChevronLeft, Plus } from "lucide-react";
import { buttonVariants } from "./ui/button";
import { Link } from "react-router";
import { cn } from "../lib/utils";

export interface PageHeaderProps {
  title: string;
  description: string;
  actionLabel?: string;
  actionPath?: string;
  buttonType?: "add" | "back"
}

export const PageHeader = ({
  title,
  description,
  actionLabel,
  actionPath,
  buttonType = "add",
}: PageHeaderProps) => {
  return (
    <div className="mb-7 flex flex-col gap-4 sm:flex-row sm:items-end sm:justify-between">
      <div>
        <h1>{title}</h1>
        <p className="mt-1 text-sm text-muted-foreground">{description}</p>
      </div>

      {actionLabel && actionPath && (
        <Link to={actionPath}
          className={cn(buttonType === "add"
            ? buttonVariants({ variant: "default" })
            : buttonVariants({ variant: "outline" })
            , "gap-2")}
        >
          {buttonType === "add"
            ? <Plus className="size-4" />
            : <ChevronLeft className="size-4" />
          }
          {actionLabel}
        </Link>
      )}
    </div>
  );
}