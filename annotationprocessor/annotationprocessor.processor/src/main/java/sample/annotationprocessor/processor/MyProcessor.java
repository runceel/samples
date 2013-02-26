/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package sample.annotationprocessor.processor;

import java.io.IOException;
import java.io.Writer;
import java.util.Set;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.annotation.processing.AbstractProcessor;
import javax.annotation.processing.Filer;
import javax.annotation.processing.RoundEnvironment;
import javax.annotation.processing.SupportedAnnotationTypes;
import javax.annotation.processing.SupportedSourceVersion;
import javax.lang.model.SourceVersion;
import javax.lang.model.element.Element;
import javax.lang.model.element.TypeElement;
import javax.tools.JavaFileObject;

/**
 *
 * @author Kazuki
 */
@SupportedSourceVersion(SourceVersion.RELEASE_6)
@SupportedAnnotationTypes({"sample.annotationprocessor.processor.Sample"})
public class MyProcessor extends AbstractProcessor {

    @Override
    public boolean process(Set<? extends TypeElement> annotations, RoundEnvironment roundEnv) {
        Filer filer = this.processingEnv.getFiler();
        int i = 0;
        for (TypeElement elm : annotations) {
            try {
                System.out.println("MyProcessor: " + elm);
                Set<? extends Element> elms = roundEnv.getElementsAnnotatedWith(elm);
                JavaFileObject file = filer.createSourceFile("org.generated.sample" + i + ".Sample");
                Writer w = file.openWriter()
                        .append("package org.generated.sample" + i + ";")
                        .append("public class Sample {}");
                w.close();
            } catch (IOException ex) {
                Logger.getLogger(MyProcessor.class.getName()).log(Level.SEVERE, null, ex);
            }
            
        }
        return true;
    }
}
