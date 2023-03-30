import React, { useState, useEffect, FC } from "react";

// Note: (mibui 2023-30-03) This is a sample component you can use as reference to get you
//                          started on React

// These are 'props' that you can pass to the component. It is basically just what arguments / data you can give the component
// This makes a generic component customizable. See App.tsx as example on how props can be used.
export interface SampleComponentProps {
    startCount: number;
}

const SampleComponent: FC<SampleComponentProps> = ({ startCount }) => {
    // React follows functional paradigm, so state should be considered immutable.
    // Never mutate state directly in React.
    // All component state should be instantiated through the useState hook.
    // useState returns the variable (state) + a function you can use to update the state
    // All state changes should go through function provided by useState, in this case 'setCount'
    // React will sync the state with the UI
    // Syncing is done by rerendering every time a useState instantiated state is changed
    // E.g. if we increment 'count' by one, the page will rerender
    let [count, setCount] = useState(startCount);
    let [todo, setTodo] = useState("");

    // Use effect is used to performing side effect such as fetching from API, setting up subscriptions etc.
    useEffect(() => {
        let getTodo = async (): Promise<any> => {
            let res = await fetch(`https://dummyjson.com/todos/${count}`);
            let todo = await res.json();
            setTodo(todo.todo);
        };
        getTodo().catch((err) => console.error(err));
        // Notice that we pass an array. This is a dependency array.
        // Everytime the state inside the dependency array changes, the code inside useEffect will
        // trigger again.
        // E.g. if we put in the "count" as dependency, then everytime we increment the count, it will retrigger the
        // getTodo method.
    }, [count]);

    let onIncrementCountClick = () => {
        // Notice we use the setCount to increment state instead of just doing count++;
        // This is to ensure that React handles the state changing + syncing everything
        setCount(count + 1);
    };

    // Notice here every time we want to reference some state or anything Javascript related,
    // we put it into brackets {}. You can essentially write Javascript directly in the HTML templating
    return (
        <>
            <div className="todo">{todo}</div>
            <div className="counter">{count}</div>
            <div>1 + 2 = {1 + 2}</div>
            <button
                onClick={onIncrementCountClick}
                className="increment-count-btn"
            >
                Increase count
            </button>
        </>
    );
};

export default SampleComponent;
