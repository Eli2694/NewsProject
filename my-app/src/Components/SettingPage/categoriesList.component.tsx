import React from "react";
import { Category } from "../../Models/Category";
import "./categoriesList.css";
interface Props {
  categories: Category[];
  newsWebsiteName: string;
  selectedCategories: Category[];
  onSelectedCategoriesChange: (categories: Category[]) => void;
}

export const CategoriesList = ({
  categories,
  newsWebsiteName,
  selectedCategories,
  onSelectedCategoriesChange,
}: Props) => {
  const filteredCategories: Category[] = categories.filter(
    (category) => category.source === newsWebsiteName
  );

  const handleCategoryClick = (category: Category) => {
    if (selectedCategories.length < 3) {
      const newSelectedCategories = [...selectedCategories, category];
      onSelectedCategoriesChange(newSelectedCategories);
    }
  };

  const handleCategoryUnclick = (category: Category) => {
    const newSelectedCategories = selectedCategories.filter(
      (c) => c.id !== category.id
    );
    onSelectedCategoriesChange(newSelectedCategories);
  };

  const handleCheckboxChange = (
    event: React.ChangeEvent<HTMLInputElement>,
    category: Category
  ) => {
    if (event.target.checked) {
      handleCategoryClick(category);
    } else {
      handleCategoryUnclick(category);
    }
  };

  const isCategorySelected = (category: Category) => {
    return selectedCategories.some((c) => c.id === category.id);
  };

  return (
    <div className="categories-list-container">
      <span className="news-website">{newsWebsiteName}</span>
      <ul>
        {filteredCategories.map((category) => (
          <li key={category.id}>
            <label>
              <input
                type="checkbox"
                checked={isCategorySelected(category)}
                onChange={(event) => handleCheckboxChange(event, category)}
              />
              <span className="category-name">{category.name}</span>
            </label>
          </li>
        ))}
      </ul>
    </div>
  );
};
