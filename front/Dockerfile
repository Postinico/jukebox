### Multi Stage Build ###

### Estágio 1 - Obter o source e gerar o build ###
FROM node:16-alpine3.12
WORKDIR /app
COPY . .
RUN npm install --force
RUN npm run build --force

### Estágio 2 - Subir o source para o servidor NGINX com a app Angular ###
##FROM nginx
##COPY nginx.conf /etc/nginx/nginx.conf
##COPY --from=ng-builder /app/dist/juke-box /usr/share/nginx/html

EXPOSE 8080