import React from "react";
import {ComponentPreview, Previews} from "@react-buddy/ide-toolbox";
import {PaletteTree} from "./palette";
import IndexPage from "../pages/IndexPage";

const ComponentPreviews = () => {
    return (
        <Previews palette={<PaletteTree/>}>
            <ComponentPreview path="/IndexPage">
                <IndexPage/>
            </ComponentPreview>
        </Previews>
    );
};

export default ComponentPreviews;