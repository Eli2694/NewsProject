import React, { useState, useEffect } from "react";
import { GetListOfNewsCategories } from "../../Services/CategoryService/category.services";
import { Category } from "../../Models/Category";
import { CategoriesList } from "./categoriesList.component";
import "./categories.css";
import { useAuth0 } from "@auth0/auth0-react";
import {
  getUserCategories,
  LoginUser,
  UpdateUserCategoriesInDB,
} from "../../Services/UserService/user.services";
import { Users } from "../../Models/Users";

export const Categories = () => {
  const [categoriesList, setCategoriesList] = useState<Category[]>([]);
  const [selectedCategories, setSelectedCategories] = useState<Category[]>([]);
  const { user } = useAuth0();
  const newsWebsites = ["walla", "ynet", "maariv", "globes"];

  const InitUserDB = async () => {
    let email: string | undefined = user?.email;
    await LoginUser(email);
  };

  useEffect(() => {
    InitUserDB();
    FetchCategories();
    FetchUserCategories();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const FetchCategories = async () => {
    try {
      const response: Category[] = await GetListOfNewsCategories();
      console.log(response);
      if (response) {
        setCategoriesList(response);
      }
    } catch (error) {
      console.error(error);
    }
  };

  const FetchUserCategories = async () => {
    try {
      let email: string | undefined = user?.email;
      const userCategories = await getUserCategories(email);
      setSelectedCategories(userCategories);
    } catch (error) {
      console.log(error);
    }
  };

  const handleSelectedCategoriesChange = (categories: Category[]) => {
    setSelectedCategories(categories);
  };

  const UpdateUser = () => {
    const categoriesCount = 3;
    if (!user?.email) {
      console.log("User email is not defined");
      return;
    }
    if (selectedCategories.length !== categoriesCount) {
      alert(`Please choose exactly ${categoriesCount} categories`);
      return;
    }
    try {
      let putUser: Users = {
        Id: 0,
        Email: user?.email,
        FirstCategoryID: selectedCategories[0].id,
        SecondCategoryID: selectedCategories[1].id,
        ThirdCategoryID: selectedCategories[2].id,
      };
      UpdateUserCategoriesInDB(putUser);
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <div className="categories-container">
      <button className="confirm-categories-btn" onClick={() => UpdateUser()}>
        Confirm Categories
      </button>
      <div className="checkbox-container">
        {newsWebsites.map((newsWebsite) => (
          <div key={newsWebsite}>
            {
              <CategoriesList
                categories={categoriesList}
                newsWebsiteName={newsWebsite}
                selectedCategories={selectedCategories}
                onSelectedCategoriesChange={handleSelectedCategoriesChange}
              ></CategoriesList>
            }
          </div>
        ))}
      </div>
    </div>
  );
};
